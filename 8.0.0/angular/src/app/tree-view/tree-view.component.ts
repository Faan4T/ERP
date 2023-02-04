import { Component, OnInit } from '@angular/core';
import { StudentServiceProxy, StudentTreeView } from '@shared/service-proxies/service-proxies';
import { TreeNode } from 'primeng/api';

@Component({
  selector: 'tree-view',
  templateUrl: './tree-view.component.html',
  styles: [
  ]
})
export class TreeViewComponent implements OnInit {

  studentsTree: StudentTreeView[];

  constructor(
    private _studentService: StudentServiceProxy,
  ) {

  }

  ngOnInit() {
      this._studentService.studentTreeView().subscribe((result) => {
        this.studentsTree = result;
      });
  }

}
