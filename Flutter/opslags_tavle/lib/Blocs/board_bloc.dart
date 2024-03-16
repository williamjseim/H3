import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

sealed class BoardEvent {}

class DraggableDropped extends BoardEvent {
  DraggableDropped(this.offset, this.image);
  Offset offset;
  String image;
}

class OpenCloseEvent extends BoardEvent {}
class ClearEvent extends BoardEvent {}

class BoardBloc extends Bloc<BoardEvent, BoardData>{
  BoardBloc(super.initialState){
    on<OpenCloseEvent>((event, emit) { state.ImageWidgetOpen = !state.ImageWidgetOpen; emit(state); });
    on<DraggableDropped>((event, emit) {
      Widget pos = Positioned(
        top: event.offset.dy,
        left: event.offset.dx,
        child: Image.memory(base64Decode(event.image))
      );

      state.widgets.add(pos);
      emit(state);
    });
  }
  
}

class BoardData{
  BoardData();
  List<Widget> widgets = List<Widget>.empty();
  bool ImageWidgetOpen = false;
  
}