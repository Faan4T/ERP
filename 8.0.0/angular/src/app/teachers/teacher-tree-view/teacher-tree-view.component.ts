import { Component, OnInit } from '@angular/core';
import { TeacherServiceProxy, TeacherTreeViews } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'teacher-tree-view',
  templateUrl: './teacher-tree-view.component.html',
  styles: [
  ]
})
export class TeacherTreeViewComponent implements OnInit {

  teacherTree: TeacherTreeViews[];

  constructor(
    private _teacherService: TeacherServiceProxy,
  ) {

  }

  ngOnInit() {
      this._teacherService.teacherTreeViews().subscribe((result) => {
        this.teacherTree = result;
      });
  }

}
