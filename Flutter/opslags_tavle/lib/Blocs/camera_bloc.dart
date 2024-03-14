import 'dart:convert';

import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

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
      images = List<String>.from(json.decode(list!) as List<dynamic>);
    }
    else{
      images = [];
    }
    images.add(base64);
    await storage.write(key: "Images", value: json.encode(images));
  }
}