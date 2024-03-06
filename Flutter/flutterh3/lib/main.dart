import 'package:flutter/material.dart';
import 'package:flutterh3/Provider/counterProvider.dart';
import 'package:provider/provider.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MultiProvider(providers: [
      ChangeNotifierProvider(create: (context)=>CounterProvider())
    ],
    child: const MyHomePage(title: 'title'));
  }
}

class MyHomePage extends StatefulWidget {
  const MyHomePage({super.key, required this.title});

  // This widget is the home page of your application. It is stateful, meaning
  // that it has a State object (defined below) that contains fields that affect
  // how it looks.

  // This class is the configuration for the state. It holds the values (in this
  // case the title) provided by the parent (in this case the App widget) and
  // used by the build method of the State. Fields in a Widget subclass are
  // always marked "final".

  final String title;

  @override
  State<MyHomePage> createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  CounterProvider? provider;
  void _incrementCounter() {
    provider!.increment();
  }

  void _decrementCounter(){
    if(provider!.counter.counter > 0){
      provider!.decrement();
    }
  }

  @override
  Widget build(BuildContext context) {
    provider = context.read<CounterProvider>();
    return MaterialApp(
      home: Scaffold(
        appBar: AppBar(
          // TRY THIS: Try changing the color here to a specific color (to
          // Colors.amber, perhaps?) and trigger a hot reload to see the AppBar
          // change color while the other colors stay the same.
          backgroundColor: Theme.of(context).colorScheme.inversePrimary,
          // Here we take the value from the MyHomePage object that was created by
          // the App.build method, and use it to set our appbar title.
          title: Text(widget.title),
        ),
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
                    provider!.counter.counter.toString(),
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
                            backgroundColor: const Color.fromARGB(255, 0, 0, 0),
                            onPressed: _incrementCounter,
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
                            backgroundColor: const Color.fromARGB(255, 0, 0, 0),
                            onPressed: _decrementCounter,
                            tooltip: 'Increment',
                            child: const Icon(Icons.remove, color: Color.fromARGB(255, 9, 255, 0),),
                          ),
                        ),
                      )
                    ],
                  ), 
                ],
              ),
            ],
          ),
        ),
        /*floatingActionButton: FloatingActionButton(
          onPressed: _incrementCounter,
          tooltip: 'Increment',
          child: const Icon(Icons.add),
        ), // This trailing comma makes auto-formatting nicer for build methods.*/
      ),
    );
  }
}
