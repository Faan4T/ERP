import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { TeacherDto, TeacherServiceProxy } from '@shared/service-proxies/service-proxies';
import * as moment from 'moment';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'edit-teacher',
  templateUrl: './edit-teacher.component.html',
  styles: [
  ]
})
export class EditTeacherComponent extends AppComponentBase
implements OnInit {
saving = false;
teacher: TeacherDto = new TeacherDto();
teacherid: number;
dateTime : string;
date:'';
departmentName: string;
@Output() onSave = new EventEmitter<any>();

constructor(
  injector: Injector,
  public _teacherService: TeacherServiceProxy,
  public bsModalRef: BsModalRef
) {
  super(injector);
}

ngOnInit(): void {
  this._teacherService.getById(this.teacherid).subscribe((result: TeacherDto) => {

    this.teacher = result;
    // binding date in input field
    this.dateTime = (new Date(result.dateOfBirth.format("YYYY-MM-DD"))).toISOString();
    this.departmentName= result.departments.departmentName;

    });
}

saveform(): void {
  this.saving = true;
  this.teacher.dateOfBirth = moment(this.date).utcOffset(0, false);

  this._teacherService.updateTeacherRecord(this.teacher).subscribe(
    () => {
      this.notify.info(this.l('Teacher Record Edited Successfully'));
      this.bsModalRef.hide();
      this.onSave.emit();
    },
    () => {
      this.saving = false;
    }
  );
}
}

