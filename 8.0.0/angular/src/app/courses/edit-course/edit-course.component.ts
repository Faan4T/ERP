import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { CourseDto, CourseServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'edit-course',
  templateUrl: './edit-course.component.html',
  styles: [
  ]
})
export class EditCourseComponent extends AppComponentBase
implements OnInit {
saving = false;
id: number;
course = new CourseDto();
checkedRolesMap: { [key: string]: boolean } = {};


@Output() onSave = new EventEmitter<any>();
  notify: any;

constructor(
  injector: Injector,
  private _courseService: CourseServiceProxy,
  public bsModalRef: BsModalRef
) {
  super(injector);
}

ngOnInit(): void {
  this._courseService

    if (this.id > 0) {
      this._courseService.getById(this.id).subscribe((result) => {
        this.course = result;
        console.log(result);
      });
    }
}
save(): void {
  this.saving = true;

  const course = new CourseDto();
  course.init(this.course);

  this._courseService.updateCourse(course).subscribe(
    () => {
      this.notify.info(this.l('SavedSuccessfully'));
      this.bsModalRef.hide();
      this.onSave.emit();
    },
    () => {
      this.saving = false;
    }
  );
}
  l(arg0: string): any {
    throw new Error('Method not implemented.');
  }
}

