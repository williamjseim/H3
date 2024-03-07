
import 'package:flutter/material.dart';

class CounterProvider extends ChangeNotifier{
  CounterObject counter = CounterObject(0);

  void increment(){
    counter = CounterObject(counter.counter + 1);
    notifyListeners();
  }

  void decrement(){
    if(counter.counter > 0){
      this.counter = CounterObject(counter.counter - 1);
      notifyListeners();
    }
  }
}


class CounterObject{
  final int _counter;
  get counter => _counter;
  CounterObject(this._counter,);
}