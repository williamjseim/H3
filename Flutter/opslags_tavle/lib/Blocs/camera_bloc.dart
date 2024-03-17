import 'dart:convert';
import 'dart:isolate';

import 'package:flutter/foundation.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:universal_io/io.dart';

  sealed class CameraEvent {}

  class NewImageEvent extends CameraEvent {
    NewImageEvent(this.base64);
    String base64;
  }

//state is the last picture taken
  class CameraBloc extends Bloc<CameraEvent, String>{
    CameraBloc() : super(""){
      on<NewImageEvent>((event, emit) async { await AddImage(event.base64); emit(event.base64); });
    }

    //add image to localstorage
  Future<void> AddImage(String base64) async{
    FlutterSecureStorage storage = const FlutterSecureStorage();
    List<String> images;
    if(await storage.containsKey(key: "Images")){
      var list = await storage.read(key: "Images");
      images = await compute<String, List<String>>((message) => List<String>.from(json.decode(message) as List<dynamic>), list!);
      //images = List<String>.from(json.decode(list!) as List<dynamic>);
    }
    else{
      images = [];
    }
    images.add(base64);
    String jsonString = await compute<List<String>, String>((message) => json.encode(message), images);
    await storage.write(key: "Images", value: jsonString);
    //await _isolateUpload(base64);
    //Isolate.spawn(_isolateUpload, base64);
  }

  Future<void> _isolateUpload(String base64) async{
    HttpClient client = HttpClient();
    Uri uri;
    if(Platform.isAndroid){
      uri = Uri.http("10.0.2.2:5142", "/Image", {"Image" : base64});  
    }
    else{
      uri = Uri.http("localhost:5142", "/Image", {"Image" : base64});  
    }

      var request = await client.postUrl(uri);
      var response = await request.close();
    }
  }