import 'dart:convert';
import 'package:flutter/foundation.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

sealed class ImageEvent {}
class GetLocalImages extends ImageEvent {}
class GetApiImages extends ImageEvent {}

class ImageBloc extends Bloc<ImageEvent, ImageData>{
  ImageBloc(super.initialState){
    on<GetLocalImages>((event, emit) async {
      state.localImages = await getImages();
      emit(state);
    });
  }

  Future<List<String>> getImages() async{
    FlutterSecureStorage storage = const FlutterSecureStorage();
    List<String> images;
    if(await storage.containsKey(key: "Images")){
      var list = await storage.read(key: "Images");
      images = await compute<String, List<String>>((message) => List<String>.from(json.decode(message) as List<dynamic>), list!);
      return images;
    }
    else{
      images = [];
    }
    return images;
  }

}

class ImageData{
  List<String> localImages = [];
  List<String> apiImages = [];
}