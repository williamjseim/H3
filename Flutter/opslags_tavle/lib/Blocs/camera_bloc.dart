
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:opslags_tavle/Repositories/repository.dart';
import 'package:opslags_tavle/Repositories/secureLocalStorageRepository.dart';

  sealed class CameraEvent {}

  class NewImageEvent extends CameraEvent {
    NewImageEvent(this.base64);
    String base64;
  }

//state is the last picture taken
  class CameraBloc extends Bloc<CameraEvent, String>{
    final Repository _repository = SecureLocalStorageRepository();
    CameraBloc() : super(""){
      on<NewImageEvent>((event, emit) async { await _repository.saveImage("Images", event.base64); emit(event.base64); });
    }

    //add image to localstorage
  Future<void> AddImage(String base64) async{
      _repository.saveImage("Images", base64);
    }
}