import { Component, Injector } from '@angular/core';
import { PagedRequestDto, PagedListingComponentBase } from '@shared/paged-listing-component-base';
import { DepartmentDto, DepartmentServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs';
import { CreateDepartmentComponent } from './create-department/create-department.component';
import { EditDepartmentComponent } from './edit-department/edit-department.component';
class PagedDepartmentsRequestDto extends PagedRequestDto {
  keyword: string;
}
@Component({
  selector: 'departments',
  templateUrl: './departments.component.html',
  styles: [
  ]
})
export class DepartmentsComponent extends PagedListingComponentBase<DepartmentDto> {
  departments: DepartmentDto[] = [];
  keyword = '';
  searchText: DepartmentDto;
  page: Number = 1;
  count: Number = 5;


  constructor(
    injector: Injector,
    private _departmentsService: DepartmentServiceProxy,
    private _modalService: BsModalService
  ) {
    super(injector);
  }

  list(
    request: PagedDepartmentsRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;

    this._departmentsService
      .getAll(request.keyword,request.skipCount,request.maxResultCount)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result) => {
        this.departments = result;
      });
  }

  delete(Department: DepartmentDto): void {
    abp.message.confirm(
      this.l('Deleting '+ Department.departmentName+" "+"("+Department.departmentCode+")"),
      undefined,
      (result: boolean) => {
        if (result) {
          this._departmentsService
            .deleteDepartment(Department.id)
            .pipe(
              finalize(() => {
                abp.notify.success(this.l('Successfully Deleted'));
                this.refresh();
              })
            )
            .subscribe(() => {});
        }
      }
    );
  }

  createDepartment(): void {
    this.showCreateOrEditDepartmentDialog();
  }

  editDepartment(Department: DepartmentDto): void {
    this.showCreateOrEditDepartmentDialog(Department.id);
  }

  showCreateOrEditDepartmentDialog(id?: number): void {
    let createOrEditDepartmentDialog: BsModalRef;
    if (!id) {
      createOrEditDepartmentDialog = this._modalService.show(
        CreateDepartmentComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditDepartmentDialog = this._modalService.show(
        EditDepartmentComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }

    createOrEditDepartmentDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }
}

