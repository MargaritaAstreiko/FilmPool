import {Input, Pipe, PipeTransform} from '@angular/core';

@Pipe({
  name: 'search'
})
export class SearchPipe implements PipeTransform {
  @Input() items=[]
  public transform(value: any, keys: string, term: string) {

    if (!term) return value;
    return (value || []).filter((item: any)=> keys.split(',').some(key => item.hasOwnProperty(key) && new RegExp(term, 'gi').test(item[key])));

  }
}