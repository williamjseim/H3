import 'dart:convert';

import 'package:camera/camera.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:opslags_tavle/Blocs/camera_bloc.dart';
import 'package:opslags_tavle/Widgets/custom_widgets.dart';

class CameraScreen extends StatelessWidget{
  const CameraScreen({super.key});

  @override
  Widget build(BuildContext context) {
    late CameraController? controller;
    return Scaffold(
      backgroundColor: Colors.black87,
      body: MultiBlocProvider(
        providers: [BlocProvider(create: (a) => CameraBloc())],
        child: Center(
          child: LayoutBuilder(builder: (context, constraints) {
            return Column(
              children: [
                tabBar(height: constraints.maxHeight * 0.1),
                Container(
                  height: constraints.maxHeight * 0.8,
                  child: Center(
                    child: FutureBuilder(future: _cameraPreview(), builder:(context, snapshot) {
                      if(!snapshot.hasData){
                        return Center(
                          child: CircularImageSpinner(duration: const Duration(seconds: 5)),
                          );
                      }
                      else{
                        controller = snapshot.data!.$2;
                        return snapshot.data!.$1;
                      }
                    },),
                  ),
                ),
                Container(
                  height: constraints.maxHeight * 0.1,
                  child: LayoutBuilder(
                    builder: (context, constraints) {
                      return Row(children: [
                        SizedBox(
                          width: constraints.maxWidth * 0.33,
                          child: Center(
                            child: BlocConsumer<CameraBloc, String>(
                              listener: (context, state) { },
                              builder: (context, lastestImage) {
                                if(lastestImage == ""){
                                  return Container();
                                }
                                else{
                                  return Image.memory(base64Decode(lastestImage));
                                }
                              }
                            )
                            )
                          ),
                        Container(
                          width: constraints.maxWidth *0.33,
                          child: Center(
                            child: FloatingActionButton(
                              child: const Icon(Icons.camera),
                              onPressed: () async{
                                if(controller != null){
                                  var file = await controller!.takePicture();
                                  file.readAsBytes().then((value) => {
                                    context.read<CameraBloc>().add(NewImageEvent(base64Encode(value)))
                                  });
                                }
                              },
                            ),
                          ),
                        ),
                      ],);
                    }
                  ),
                ),
              ],
            );
          },),
        ),
      ),
    );
  }
}

Future<(Widget, CameraController)> _cameraPreview() async{
  Widget widget = Text("fuck");
  var cameras = await availableCameras();
  await Future.delayed(const Duration(seconds: 2));
  var controller = CameraController(cameras[0], kIsWeb ? ResolutionPreset.high : ResolutionPreset.medium, enableAudio: false, imageFormatGroup: ImageFormatGroup.jpeg);
  await controller.initialize().then((value) {
    widget = CameraPreview(controller);
  },).catchError((e){
    if(e is CameraException){
      widget = const Text("No camera available", style: TextStyle(color: Colors.white),);
    }
  });
  return (widget, controller);
}