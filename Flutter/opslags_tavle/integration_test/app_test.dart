import 'dart:async';
import 'dart:io';

import 'package:flutter/material.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:integration_test/integration_test.dart';
import 'package:integration_test/integration_test_driver.dart';
import 'package:opslags_tavle/main.dart';

void main(){

  group("integration tests", () { 
    IntegrationTestWidgetsFlutterBinding.ensureInitialized();
    testWidgets("Image taking test", (widgetTester) async {
      await widgetTester.pumpWidget(const MyApp());
      expect(find.byIcon(Icons.menu), findsOneWidget);

      await widgetTester.pumpAndSettle();
      
      final fab = find.byIcon(Icons.menu);

      await widgetTester.tap(fab);

      await widgetTester.pumpAndSettle();

      final camera = find.byIcon(Icons.camera);

      await widgetTester.tap(camera);

      await widgetTester.pumpAndSettle(Duration(seconds: 2));


    });
  });
}