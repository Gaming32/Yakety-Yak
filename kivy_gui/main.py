from kivy.app import App
from kivy.uix.anchorlayout import AnchorLayout
from kivy.uix.boxlayout import BoxLayout
from kivy.uix.label import Label
from kivy.uix.button import Button
from kivy.uix.textinput import TextInput

from kivy.core.window import Window
Window.clearcolor = (0.94117647058, 0.94117647058, 0.66666666666, 1.0)


class MainScreen(BoxLayout):

    def __init__(self, **kwargs):
        super().__init__(orientation='vertical', **kwargs)
        ip_label = Label(text='IP Address:', color=(0, 0, 0, 1))
        self.ip_address = TextInput(multiline=False)
        self.connect_button = Button(text='Connect')
        self.add_widget(ip_label)
        self.add_widget(self.ip_address)
        self.add_widget(self.connect_button)


class MainWindow(AnchorLayout):

    def __init__(self, **kwargs):
        super().__init__(anchor_x='center', anchor_y='center', **kwargs)
        self.main_screen = MainScreen(size_hint=(.5, .2))
        self.add_widget(self.main_screen)


class YaketyYakApp(App):

    def build(self):
        self.title = 'Yakety Yak'


if __name__ == '__main__':
    YaketyYakApp().run()