import 'package:flutter/material.dart';
import 'package:flutterh3/Provider/counterProvider.dart';
import 'package:flutterh3/main.dart';
import 'package:provider/provider.dart';

class ProviderWidget extends StatelessWidget{
  @override
  Widget build(BuildContext context) {
    return MultiProvider(
        providers: [
          ChangeNotifierProvider(create: (context)=> CounterProvider())
        ],
        child: Scaffold(
          appBar: mainappbar(),
          body: Center(
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: <Widget>[
                      const Text(
                        'You have pushed the button this many times:',
                      ),
                      Consumer<CounterProvider>(
                        builder: (context, value, child){
                          return Text(
                            value.counter.counter.toString(),
                            style: Theme.of(context).textTheme.headlineMedium,
                          );
                        },
                      ),
                      Column(
                        mainAxisAlignment: MainAxisAlignment.center,
                        children: [
                          Row(
                            mainAxisAlignment: MainAxisAlignment.center,
                            children: [
                              Container(
                                padding: const EdgeInsets.all(10),
                                child: Theme(
                                  data: ThemeData(
                                    shadowColor: const Color.fromARGB(255, 9, 255, 0)
                                  ),
                                  child: FloatingActionButton(
                                    heroTag: "button1",
                                    backgroundColor: const Color.fromARGB(255, 0, 0, 0),
                                    onPressed: context.read<CounterProvider>().increment,
                                    tooltip: 'Increment',
                                    child: const Icon(Icons.add, color: Color.fromARGB(255, 9, 255, 0),),
                                  ),
                                  ),
                                ), 
                              Container(
                                padding: const EdgeInsets.all(10),
                                child: Theme(
                                  data: ThemeData(
                                    shadowColor: const Color.fromARGB(255, 9, 255, 0)
                                  ),
                                  child: FloatingActionButton(
                                    heroTag: "button2",
                                    backgroundColor: const Color.fromARGB(255, 0, 0, 0),
                                    onPressed: context.read<CounterProvider>().decrement,
                                    tooltip: 'Decrement',
                                    child: const Icon(Icons.remove, color: Color.fromARGB(255, 9, 255, 0),),
                                  ),
                                  ),
                                ),
                            ]),
                          ]), 
                        ],
                      ),
                    ),
        ),
      );
  }

}