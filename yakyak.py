import sys
import socket, pyaudio


CHUNK = 1024
WIDTH = 2
CHANNELS = 1
RATE = 44100
PORT = 9225


SERVER = None
if len(sys.argv) > 1:
    SERVER = sys.argv[1].rsplit(':', 1)
    if len(SERVER) < 2:
        SERVER.append('')
    if not SERVER[1]:
        SERVER[1] = PORT
    if not SERVER[0]:
        SERVER[0] = '127.0.0.1'
    try: SERVER[1] = int(SERVER[1])
    except ValueError:
        print('invalid port:', repr(SERVER[1]))
        sys.exit(1)
    SERVER = tuple(SERVER)


p = pyaudio.PyAudio()

stream = p.open(format=pyaudio.paInt24,
                channels=CHANNELS,
                rate=RATE,
                input=True,
                output=True,
                frames_per_buffer=CHUNK)

import atexit
def exit_handler():
    stream.stop_stream()
    stream.close()

    p.terminate()


def get_addr_string(addr):
    return ':'.join(str(x) for x in addr)


def recv_exact(sockobj:socket.socket, bc:int, chunk=8192):
    left = min(bc, chunk)
    data = b''
    while left:
        newbc = min(chunk, left)
        try: newdata = sockobj.recv(newbc)
        except ConnectionError: break
        data += newdata
        left -= newbc
    return data


if SERVER is None:
    server = socket.socket()
    server.bind(('', PORT))
    server.listen(0)
    print('listening on', get_addr_string(server.getsockname()))
    sockobj, addr = server.accept()
    print('connection accepted from', get_addr_string(addr))

else:
    sockobj = socket.socket()
    print('attempting to connect to', get_addr_string(SERVER))
    sockobj.connect(SERVER)
    print('connected successfully')


try:
    while True:
        mydata = stream.read(CHUNK)
        mydata_length = len(mydata).to_bytes(4, 'little')
        try: sockobj.send(mydata_length + mydata)
        except ConnectionError: break

        yourdata_length = int.from_bytes(recv_exact(sockobj, 4), 'little')
        yourdata = recv_exact(sockobj, yourdata_length)
        if len(yourdata) < yourdata_length:
            break
        stream.write(yourdata)

except KeyboardInterrupt: print('hanging up')

print('conversation ended')
