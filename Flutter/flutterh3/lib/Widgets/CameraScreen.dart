import 'dart:convert';

import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:flutterh3/main.dart';
import 'package:camera/camera.dart';

class Camera extends StatefulWidget{
  const Camera({super.key});

  @override
  State<Camera> createState() => _CameraState();
}

class _CameraState extends State<Camera> with WidgetsBindingObserver, TickerProviderStateMixin{
  CameraController? controller;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: mainappbar(context),
      body: Center(
        child: FutureBuilder(
          future: _cameraPreviewWidget(),
          builder: ( context, snapshot)
          {
            return Center(
              child: Column(
                children: [
                  if(snapshot.data != null)
                    snapshot.data!
                    else
                    Text("adswawd")
                ],
              ),
            );
          }
        ),
      ),
    );
  }

   Future _cameraPreviewWidget() async {
    var cameras = await availableCameras();
    const String text = "asss";
    CameraController camera = CameraController(
      cameras[0],
      kIsWeb ? ResolutionPreset.high : ResolutionPreset.medium,
      enableAudio: false,
      imageFormatGroup:  ImageFormatGroup.jpeg,
    );
    await camera.initialize();
    return Column(
      children: [
        CameraPreview(
          camera,
        ),
        FloatingActionButton(
          
          onPressed: () {  camera.setFocusMode(FocusMode.locked).then((value) { camera.takePicture().then((value) => {
            value.readAsBytes().then((bytes)=>{
            FlutterSecureStorage().write(key: "Image", value: base64Encode(bytes)),
            print("Success"),
            }),
          });
          });
          },
          child: const Icon(Icons.camera),
        )
      ],
    );
  }

  _initializeCamera(CameraDescription desc){
    final CameraController _controller = CameraController(
      desc,
      kIsWeb ? ResolutionPreset.high : ResolutionPreset.medium,
      enableAudio: false,
      imageFormatGroup:  ImageFormatGroup.jpeg,
    );

    controller = _controller;

  }

}