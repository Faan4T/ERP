import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { CourseDto, CourseServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'create-course',
  templateUrl: './create-course.component.html',
  styles: [
  ]
})
export class CreateCourseComponent extends AppComponentBase
implements OnInit {
saving = false;
course = new CourseDto();
checkedRolesMap: { [key: string]: boolean } = {};
// permissions: PermissionDto[] = [];
// checkedPermissionsMap: { [key: string]: boolean } = {};
// defaultPermissionCheckedStatus = true;
id: number;

@Output() onSave = new EventEmitter<any>();

constructor(
  injector: Injector,
  private _courseService: CourseServiceProxy,
  public bsModalRef: BsModalRef
) {
  super(injector);
}

ngOnInit(): void {
  this._courseService
    // .getAllPermissions()
    // .subscribe((result: PermissionDtoListResultDto) => {
    //   this.permissions = result.items;
    //   this.setInitialPermissionsStatus();
    // });
    if (this.id > 0) {
      this._courseService.getById(this.id).subscribe((result) => {
        this.course = result;
        console.log(result);
      });
    }
}

save(): void {
  // debugger
  this.saving = true;

  const course = new CourseDto();
  course.init(this.course);
  //course.grantedPermissions = this.getCheckedPermissions();

  this._courseService
    .createCourse(course)
    .subscribe(
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
}
