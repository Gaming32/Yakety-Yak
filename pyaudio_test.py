import pyaudio

CHUNK = 1024
WIDTH = 2
CHANNELS = 1
RATE = 44100
RECORD_SECONDS = 5

p = pyaudio.PyAudio()

# [print(i, ':', p.get_device_info_by_index(i)) for i in range(p.get_device_count())]
# exit()

stream = p.open(format=p.get_format_from_width(WIDTH),
                channels=CHANNELS,
                rate=RATE,
                input=True,
                output=True,
                frames_per_buffer=CHUNK)

print('* recording')

chunks = []
try:
    while True:
        data = stream.read(CHUNK)
        chunks.append(data)
        if len(chunks) >= 215:
            stream.write(chunks.pop(0), CHUNK)
        print('cpu:', stream.get_cpu_load(), end='\r')
except KeyboardInterrupt: print('exiting...')
try:
    [stream.write(chunk) for chunk in chunks]
except KeyboardInterrupt: print('EXITING!!!')

print('* done')

stream.stop_stream()
stream.close()

p.terminate()
