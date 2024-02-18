import simplepyble
import time

CHARACTERISTIC_UUID = "beb5483e-36e1-4688-b7f5-ea07361b26a8"
SERVICE_UUID = "4fafc201-1fb5-459e-8fcc-c5c9c331914b"

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
        # print(f"Peripheral: {peripheral.address()} [{peripheral.identifier()}]")
        if peripheral.identifier() == "MyESP32":
            index = i
            break

    peripheral = peripherals[index]
    # print(f"Connecting to {peripheral.address()} [{peripheral.identifier()}]")
    peripheral.connect()
    print("CONNECTED")

    while peripheral.is_connected():
        message = str(peripheral.read(SERVICE_UUID, CHARACTERISTIC_UUID), 'UTF-8')
        print(message)
        time.sleep(0.05)

    # peripheral.disconnect()

connect_to_sleeve()
