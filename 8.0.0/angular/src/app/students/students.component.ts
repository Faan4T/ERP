import { Component, Injector } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { StudentDto, StudentServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs';
import { CreateStudentComponent } from './create-student/create-student.component';
import { EditStudentComponent } from './edit-student/edit-student.component';
import { ViewCoursesComponent } from './view-courses/view-courses.component';
import { AddCoursesComponent } from './add-courses/add-courses.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { SortEvent } from 'primeng/api';

class PagedStudentRequestDto extends PagedRequestDto {
  keyword: any;

}

@Component({
  selector: 'students',
  templateUrl: './students.component.html',
  animations: [appModuleAnimation()]
})

export class StudentsComponent extends PagedListingComponentBase<StudentDto> {
  student: StudentDto[] = [];
  keyword = '';
  bsModalRef: BsModalRef;
 id : any;
//pagination parameters
 page: any = 1;
 count: any = 5;
  constructor(
    injector: Injector,
    private _studentService: StudentServiceProxy,
    private _modalService: BsModalService,
  ) {
    super(injector);
  }

  createStudent(): void {

    this.showCreateOrEditStudentDialog();
  }

  editStudent(student: StudentDto): void {

    this.showCreateOrEditStudentDialog(student.id);
  }

  clearFilters(): void {
    this.keyword = '';
    this.getDataPage(1);
  }

  list(
    request: PagedStudentRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;
    this._studentService
      .getAll(request.keyword, request.skipCount, request.maxResultCount)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result) => {
        this.student = result;
      });
  }

  protected delete(student: StudentDto): void {
    abp.message.confirm(
      this.l('Warning', student.firstName),
      undefined,
      (result: boolean) => {
        if (result) {
          this._studentService.delete(student.rollNo).subscribe(() => {
            abp.notify.success(this.l('Student Successfully Deleted'));
            this.refresh();
          });
        }
      }
    );
  }
  private showCreateOrEditStudentDialog(id?: number): void {

    let createOrEditStudentDialog: BsModalRef;
    if (!id) {

      createOrEditStudentDialog = this._modalService.show(
        CreateStudentComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditStudentDialog = this._modalService.show(
        EditStudentComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }

    createOrEditStudentDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }
  viewCourse(stud: StudentDto): void {
 debugger
   this.showViewCourseDialog(stud.id);
  }

  showViewCourseDialog(id?: number): void {

    let showViewCourse: BsModalRef;
    if (!id) {
      showViewCourse = this._modalService.show(
        ViewCoursesComponent,
        {
          class: 'modal-lg',
        }
      );
    }
    else {
      debugger
      showViewCourse = this._modalService.show(
        ViewCoursesComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }
  }
  registerCourse(student: StudentDto): void {

    this.showRegisterCourseDialog(student.id);
  }
  showRegisterCourseDialog(rollno?: any): void {

    let AddCourse: BsModalRef;
    if (rollno) {
      AddCourse = this._modalService.show(
        AddCoursesComponent,
        {
          class: 'modal-lg',
           initialState: {
            rollno: rollno
          },
        }
      );
    }
    else
    {
      abp.notify.error(this.l('Error Detected'));
    }
  }
  customSort(event: SortEvent) {
    event.data.sort((data1, data2) => {
        let value1 = data1[event.field];
        let value2 = data2[event.field];
        let result = null;

        if (value1 == null && value2 != null)
            result = -1;
        else if (value1 != null && value2 == null)
            result = 1;
        else if (value1 == null && value2 == null)
            result = 0;
        else if (typeof value1 === 'string' && typeof value2 === 'string')
            result = value1.localeCompare(value2);
        else
            result = (value1 < value2) ? -1 : (value1 > value2) ? 1 : 0;

        return (event.order * result);
    });
}
}

