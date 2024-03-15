import 'dart:io';

import 'package:integration_test/integration_test_driver.dart';

void main() async{

  await Process.run('abd',[
    'shell',
    'pm',
    'grant',
    'com.example.opslags_tavle',
    'android.permission.CAMERA'
  ]);
  await integrationDriver();
}