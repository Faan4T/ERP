import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { StudentDto, StudentServiceProxy } from '@shared/service-proxies/service-proxies';
import * as moment from 'moment';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'create-student',
  templateUrl: './create-student.component.html',
  styles: [
  ]
})
export class CreateStudentComponent extends AppComponentBase
implements OnInit {
saving = false;
student: StudentDto = new StudentDto();
date:'';
departmentList: any;
@Output() onSave = new EventEmitter<any>();

constructor(
  injector : Injector,
  public _studentService: StudentServiceProxy,
  public bsModalRef: BsModalRef,
) {
  super(injector);
}

ngOnInit(): void {
  this._studentService.getAllDepartmentDropDown().subscribe((result:any)=>
  {
    this.departmentList=result;
  })
}
Department(e)
{
  this.student.departmentId= e.target.value;
  // console.log(e.target.value);
}
saveform(): void {

  this.saving = true;
  this.student.dateOfBirth = moment(this.date).utcOffset(0, false);
  debugger
  this._studentService.createStudent(this.student).subscribe(
    () => {
      this.notify.info(this.l('Student Saved Successfully'));
      this.bsModalRef.hide();
      this.onSave.emit();
    },
    () => {
      this.saving = false;
    }
  );
}


}
