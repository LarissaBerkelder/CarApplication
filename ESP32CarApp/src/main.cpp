#include <Arduino.h>
#include "BLE2902.h"
#include "BLEDevice.h"
#include "BLEServer.h"
#include "BLEUtils.h"

#define BAUD 9600

// Pin definitions
const int motorPin1 = 21;   // Rechts voor 
const int motorPin2 = 22;   // Links achter
const int motorPin3 = 23;   // Rechts achter
const int motorPin4 = 19;   // Links voor  

// Prototypes
void MotorPinsINIT();
void HandleCommand();
void StopDriving();
void DriveForwards();
void DriveBackwards();
void TurnRight();
void TurnLeft();


// Bluetooth
BLEServer *pServer = NULL;
BLECharacteristic *pCharacteristic = NULL;
BLEDescriptor *pDescr;
BLE2902 *pBLE2902;

bool deviceConnected = false;
bool oldDeviceConnected = false;

#define SERVICE_UUID "a71ec97a-1f14-11ee-be56-0242ac120002"
#define CHARACTERISTIC_UUID "a71ecbfa-1f14-11ee-be56-0242ac120002"

// String PrevCommand = "";
String BluetoothCommand = "";

// Bluetooth callback functions
class MyServerCallbacks : public BLEServerCallbacks {
    void onConnect(BLEServer *pServer) {
        deviceConnected = true;
    };

    void onDisconnect(BLEServer *pServer) {
        deviceConnected = false;
    }
};

// Bluetooth callback function that handles incoming messages
class MyCharacteristicCallbacks : public BLECharacteristicCallbacks {
    void onWrite(BLECharacteristic *pCharacteristic) {
        uint8_t *retrieved = pCharacteristic->getData();
        const char *retrieved_message = (const char *)retrieved;
        BluetoothCommand = retrieved_message;
        //HandleCommand(retrieved_message);
    }
};

// void HandleCommand(String command){
//   if(command == "UP"){
//   }

//   else if (command == "DOWN"){
//   }

//   else if (command == "RIGHT"){
//   }

//   else if (command == "LEFT"){
//   }

//   else if (command == "STOP"){
//   }
// }

// Bluetooth initilizing
void ble_init() {
    BLEDevice::init("BLE_Car");
    pServer = BLEDevice::createServer();
    pServer->setCallbacks(new MyServerCallbacks());
    BLEService *pService = pServer->createService(SERVICE_UUID);
    pCharacteristic = pService->createCharacteristic(
        CHARACTERISTIC_UUID,
        BLECharacteristic::PROPERTY_READ |
            BLECharacteristic::PROPERTY_WRITE |
            BLECharacteristic::PROPERTY_NOTIFY);

    BLEAdvertising *pAdvertising = BLEDevice::getAdvertising();
    pAdvertising->addServiceUUID(SERVICE_UUID);

    pCharacteristic->setCallbacks(new MyCharacteristicCallbacks()); // Assign the callbacks

    pCharacteristic->setReadProperty(true);
    pCharacteristic->setNotifyProperty(true);
    pServer->getAdvertising()->start();
    pService->start();
}


void setup() {
  MotorPinsINIT();
  Serial.begin(BAUD);
  ble_init();
}


void loop() {
  if (deviceConnected) {
    if(BluetoothCommand == "UP"){
      DriveForwards();
    }
    else if (BluetoothCommand == "DOWN"){
      DriveBackwards();
    }

    else if (BluetoothCommand == "RIGHT"){
      TurnRight();
    }

    else if (BluetoothCommand == "LEFT"){
      TurnLeft();
    }

    else if (BluetoothCommand == "STOP"){
      StopDriving();
    }
  }
    // disconnecting
    if (!deviceConnected && oldDeviceConnected) {
        delay(500);                  // give the bluetooth stack the chance to get things ready
        pServer->startAdvertising(); // restart advertising
        oldDeviceConnected = deviceConnected;
    }
    // connecting
    if (deviceConnected && !oldDeviceConnected) {
        oldDeviceConnected = deviceConnected;
    }
}

void MotorPinsINIT(){
  pinMode(motorPin1, OUTPUT);
  pinMode(motorPin2, OUTPUT);
  pinMode(motorPin3, OUTPUT);
  pinMode(motorPin4, OUTPUT);
}

void StopDriving(){
  digitalWrite(motorPin1, LOW);
  digitalWrite(motorPin2, LOW);
  digitalWrite(motorPin3, LOW); 
  digitalWrite(motorPin4, LOW); 
}

void DriveForwards(){
  digitalWrite(motorPin1, HIGH);
  digitalWrite(motorPin4, HIGH); 
}

void DriveBackwards(){
  digitalWrite(motorPin2, HIGH); 
  digitalWrite(motorPin3, HIGH); 
}

void TurnRight(){
  digitalWrite(motorPin3, HIGH);
  digitalWrite(motorPin4, HIGH);
}

void TurnLeft(){
  digitalWrite(motorPin1, HIGH);
  digitalWrite(motorPin2, HIGH);
}