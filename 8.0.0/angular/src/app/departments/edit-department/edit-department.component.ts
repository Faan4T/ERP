import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { DepartmentDto, DepartmentServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'edit-department',
  templateUrl: './edit-department.component.html',
  styles: [
  ]
})
export class EditDepartmentComponent extends AppComponentBase
implements OnInit {
saving = false;
id: number;
department = new DepartmentDto();
checkedRolesMap: { [key: string]: boolean } = {};


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
  this.saving = true;

  const department = new DepartmentDto();
  department.init(this.department);

  this._departmentService.updateDepartment(department).subscribe(
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


