import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { DepartmentDto, DepartmentServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'create-department',
  templateUrl: './create-department.component.html',
  styles: [
  ]
})
export class CreateDepartmentComponent extends AppComponentBase
implements OnInit {
saving = false;
department = new DepartmentDto();
id: number;

@Output() onSave = new EventEmitter<any>();

constructor(
  injector: Injector,
  private _departmentService: DepartmentServiceProxy,
  public bsModalRef: BsModalRef
) {
  super(injector);
}

ngOnInit(): void {
  this._departmentService
    if (this.id > 0) {
      this._departmentService.getById(this.id).subscribe((result) => {
        this.department = result;
        console.log(result);
      });
    }
}

save(): void {
  // debugger
  this.saving = true;

  const department = new DepartmentDto();
  department.init(this.department);

  this._departmentService
    .createDepartment(department)
    .subscribe(
      () => {
        this.notify.info(this.l('Saved Successfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      },
      () => {
        this.saving = false;
      }
    );
}
}
