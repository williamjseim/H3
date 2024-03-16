
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

Widget barButton({required String Route, Widget? child, Color? backgroundColor = Colors.white, Color? foreGroundColor = Colors.black}){
  return Builder(
    builder: (context) {
      return ElevatedButton(
        style: ElevatedButton.styleFrom(
          shape: const RoundedRectangleBorder(
            borderRadius: BorderRadius.all(Radius.zero),
          ),
          elevation: 0.0,
          backgroundColor: backgroundColor,
          foregroundColor: foreGroundColor,
        ),
        onPressed: (){ context.go(Route); },
        child: child,
        );
    }
  );
}

class CircularImageSpinner extends StatefulWidget{

  CircularImageSpinner({ required this.duration, super.key});

  final Duration duration;
  
  @override
  State<CircularImageSpinner> createState() => _CircularImageSpinnerState();
}

class _CircularImageSpinnerState extends State<CircularImageSpinner> with SingleTickerProviderStateMixin{
  late AnimationController _controller;
  late Animation<double> _animation;

  @override
  Widget build(BuildContext context) {
    return LayoutBuilder(
      builder: (context, constraints) {
        return AnimatedBuilder(
          animation: _controller,
          builder: (context, child) {
            return Transform.rotate(angle: _animation.value, child: Image.asset("assets/koiCircle.png", height: constraints.maxWidth * 0.5, width: constraints.maxWidth * 0.5,),);
          }
        );
      }
    );
  }

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(vsync: this, duration: widget.duration);

    _animation = Tween<double>(
      begin: 0,
      end: -2 * 3.141,
    ).animate(_controller);

    _controller.repeat();
  }

  @override
  void dispose(){
    _controller.dispose();
    super.dispose();
  }

}

AppBar mainAppBar({String title = "Missing Title"}){
  return AppBar(
    title: Text(title, style: TextStyle(color: Colors.white),),
    backgroundColor: Colors.transparent,
    elevation: 0.0,
  );
}

Widget tabBar({double? height, AppBar? appBar}){
  return Container(
    height: height,
    alignment: AlignmentDirectional.bottomCenter,
    decoration: BoxDecoration(
          image: DecorationImage(repeat: ImageRepeat.repeatX, image: AssetImage("assets/koi.png")),
        ),
    child: Column(
      mainAxisAlignment: MainAxisAlignment.end,
      children: [
        if(appBar != null)
          appBar,
          LayoutBuilder(
            builder: (context, constraints) {
              return OverflowBar(
                children: [
                  Container(width: constraints.maxWidth / 3, child: barButton(Route: "/", child:Text("Home", style: TextStyle(color: Colors.white),), backgroundColor: Colors.transparent),),
                  Container(width: constraints.maxWidth / 3, child: barButton(Route: "/Camera", child:Text("Camera", style: TextStyle(color: Colors.white),), backgroundColor: Colors.transparent),),
                  Container(width: constraints.maxWidth / 3, child: barButton(Route: "/Board", child:Text("Board", style: TextStyle(color: Colors.white),), backgroundColor: Colors.transparent),),
                ],
              );
            }
        ),
      ],
    ),
  );
}