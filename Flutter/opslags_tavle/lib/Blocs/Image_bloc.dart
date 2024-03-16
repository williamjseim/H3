import 'dart:convert';
import 'dart:isolate';

import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

sealed class ImageEvent {}
class GetLocalImages extends ImageEvent {}
class GetApiImages extends ImageEvent {}

class ImageBloc extends Bloc<ImageEvent, ImageData>{
  ImageBloc(super.initialState){
    on<GetLocalImages>((event, emit) async {
      state.localImages = await GetImages();
      emit(state);
    });
  }

  Future<List<String>> GetImages() async{
    FlutterSecureStorage storage = const FlutterSecureStorage();
    List<String> images;
    if(await storage.containsKey(key: "Images")){
      var list = await storage.read(key: "Images");
      images = await compute<String, List<String>>((message) => List<String>.from(json.decode(message) as List<dynamic>), list!);
    }
    else{
      images = [];
    }
    return images;
  }

}

class ImageData{
  List<String> localImages = List.empty();
  List<String> apiImages = List.empty();
}