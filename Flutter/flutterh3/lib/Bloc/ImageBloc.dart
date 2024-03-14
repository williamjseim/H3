import 'dart:convert';
import 'dart:math';

import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

sealed class ImageEvent{}

final class NewImageEvent extends ImageEvent {
  NewImageEvent(this.mousePos, this.image);
  Point mousePos;
  String image;
}
final class ImageDownloadEvent extends ImageEvent {}

class ImageBloc extends Bloc<ImageEvent, Images>{
  ImageBloc() : super(Images([])){
    on<NewImageEvent>((event, emit) => emit(Images(state.AddImage(event.mousePos, event.image))));
    on<ImageDownloadEvent>((event, emit) => emit(state));
  }

  @override
  void onChange(Change<Images> change) {
    super.onChange(change);
  }
}

class Images {
  Images(this.images);
  List<Widget> images = <Widget>[];
  
  List<Widget> AddImage(Point mousePos, String image){
    Widget widget = Positioned(
      top: mousePos.y as double,
      left: mousePos.x as double,
      child: Image.asset(image),
      );
      images.add(widget);
      return images;
  }
}