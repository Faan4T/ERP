import { TeacherDto, TeacherServiceProxy } from '@shared/service-proxies/service-proxies';
import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import * as moment from 'moment';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'create-teacher',
  templateUrl: './create-teacher.component.html',
  styles: [
  ]
})
export class CreateTeacherComponent extends AppComponentBase
implements OnInit {
saving = false;
teacher: TeacherDto = new TeacherDto();
date:'';
departmentList: any;
@Output() onSave = new EventEmitter<any>();

constructor(
  injector : Injector,
  public _teacherService: TeacherServiceProxy,
  public bsModalRef: BsModalRef,
) {
  super(injector);
}

ngOnInit(): void {
  this._teacherService.getAllDepartmentDropDown().subscribe((result:any)=>
  {
    this.departmentList=result;
  })
}
Department(e)
{
  this.teacher.departmentId= e.target.value;
  // console.log(e.target.value);
}
saveform(): void {

  this.saving = true;
  this.teacher.dateOfBirth = moment(this.date).utcOffset(0, false);
  debugger
  this._teacherService.createTeacher(this.teacher).subscribe(
    () => {
      this.notify.info(this.l('Teacher Saved Successfully'));
      this.bsModalRef.hide();
      this.onSave.emit();
    },
    () => {
      this.saving = false;
    }
  );
}


}
