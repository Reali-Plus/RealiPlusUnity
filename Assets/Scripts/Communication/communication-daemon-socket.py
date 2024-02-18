import simplepyble
import socket
import numpy as np
import time

CHARACTERISTIC_UUID = "beb5483e-36e1-4688-b7f5-ea07361b26a8"
SERVICE_UUID = "4fafc201-1fb5-459e-8fcc-c5c9c331914b"
SOCKET_PORT = 8000


def initialize_socket():
    s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    s.connect(('127.0.0.1', SOCKET_PORT))

    return s

def connect_to_sleeve():
    print(f"Running on {simplepyble.get_operating_system()}")

    adapters = simplepyble.Adapter.get_adapters()

    if len(adapters) == 0:
        print("No adapters found")
        return

    for adapter in adapters:
        print(f"Adapter: {adapter.identifier()} [{adapter.address()}]")

    adapter = adapters[0]

    adapter.scan_for(5000)
    peripherals = adapter.scan_get_results()

    if len(peripherals) == 0:
        print("No peripherals found")
        return
    
    index = 0
    for i, peripheral in enumerate(peripherals):
        if peripheral.identifier() == "MyESP32":
            index = i
            break

    peripheral = peripherals[index]
    peripheral.connect()

    return peripheral


def send_data(sleeve, s):
    while sleeve.is_connected():
        message = str(sleeve.read(SERVICE_UUID, CHARACTERISTIC_UUID), 'UTF-8')
        print(message)
        s.send(bytes(message, 'utf-8'))
        time.sleep(0.05)


s = initialize_socket()
sleeve = connect_to_sleeve()
send_data(sleeve, s)

s.close()

