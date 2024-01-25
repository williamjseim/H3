import {Directive, Output, EventEmitter} from '@angular/core';

@Directive({
  selector: '[Onload]'
})
export class OnloadDirective {

  @Output('Onload') OnLoadEvent: EventEmitter<any> = new EventEmitter();

  ngOnInit() {
      setTimeout(() => this.OnLoadEvent.emit(), 10);
  }
}