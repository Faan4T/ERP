import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { StudentDto, StudentServiceProxy } from '@shared/service-proxies/service-proxies';
import * as moment from 'moment';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'edit-student',
  templateUrl: './edit-student.component.html',
  styles: [
  ]
})
export class EditStudentComponent extends AppComponentBase
implements OnInit {
saving = false;
student: StudentDto = new StudentDto();
id: number;
dateTime : string;
date:'';
departmentName: string;
@Output() onSave = new EventEmitter<any>();

constructor(
  injector: Injector,
  public _studentService: StudentServiceProxy,
  public bsModalRef: BsModalRef
) {
  super(injector);
}

ngOnInit(): void {
  this._studentService.getById(this.id).subscribe((result: StudentDto) => {
    this.student = result;
    // binding date in input field
    this.dateTime = (new Date(result.dateOfBirth.format("YYYY-MM-DD"))).toISOString();
    this.departmentName= result.departments.departmentName;

    });
}

saveform(): void {
  this.saving = true;
  this.student.dateOfBirth = moment(this.date).utcOffset(0, false);
  this._studentService.updateStudent(this.student).subscribe(
    () => {
      this.notify.info(this.l('Student Record Edited Successfully'));
      this.bsModalRef.hide();
      this.onSave.emit();
    },
    () => {
      this.saving = false;
    }
  );
}
}

