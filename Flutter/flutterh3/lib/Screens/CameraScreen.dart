import 'dart:convert';
import 'package:universal_io/io.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:flutterh3/Widgets/mainComponents.dart';
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
    HttpClient http = HttpClient();
    var cameras = await availableCameras();
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
          
          onPressed: () async { camera.takePicture().then((value) async => {
            value.readAsBytes().then((bytes) async{
              HttpClientResponse response = await http.postUrl(Uri.parse("http://10.0.2.2:5142/Image/${base64Encode(bytes)}")).then((value) => value.close()),

            }),
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