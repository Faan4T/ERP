import { Component, Injector } from '@angular/core';
import { AddCoursesComponent } from '@app/students/add-courses/add-courses.component';
import { ViewCoursesComponent } from '@app/students/view-courses/view-courses.component';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { TeacherDto, TeacherServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { SortEvent } from 'primeng/api';
import { finalize } from 'rxjs';
import { CreateTeacherComponent } from './create-teacher/create-teacher.component';
import { EditTeacherComponent } from './edit-teacher/edit-teacher.component';
import { AddcoursesComponent } from './addcourses/addcourses.component';
import { ViewcoursesComponent } from './viewcourses/viewcourses.component';

class PagedTeacherRequestDto extends PagedRequestDto {
  keyword: any;

}
@Component({
  selector: 'teachers',
  templateUrl: './teachers.component.html',
  styles: [
  ]
})
export class TeachersComponent extends PagedListingComponentBase<TeacherDto> {
  teacher: TeacherDto[] = [];
  keyword = '';
  bsModalRef: BsModalRef;
  teacherid : any;
//pagination parameters
 page: any = 1;
 count: any = 5;
  constructor(
    injector: Injector,
    private _teacherService: TeacherServiceProxy,
    private _modalService: BsModalService,
  ) {
    super(injector);
  }

  createTeacher(): void {

    this.showCreateOrEditTeacherDialog();
  }

  editTeacher(teacher: TeacherDto): void {

    this.showCreateOrEditTeacherDialog(teacher.id);
  }

  clearFilters(): void {
    this.keyword = '';
    this.getDataPage(1);
  }

  list(
    request: PagedTeacherRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;
    this._teacherService
      .getAll(request.keyword, request.skipCount, request.maxResultCount)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result) => {
        this.teacher = result;
      });
  }

  protected delete(teacher: TeacherDto): void {
    abp.message.confirm(
      this.l('Warning', teacher.firstName),
      undefined,
      (result: boolean) => {
        if (result) {
          this._teacherService.delete(teacher.teacherId).subscribe(() => {
            abp.notify.success(this.l('Teacher Successfully Deleted'));
            this.refresh();
          });
        }
      }
    );
  }
  private showCreateOrEditTeacherDialog(id?: any): void {

    let createOrEditTeacherDialog: BsModalRef;
    if (!id) {

      createOrEditTeacherDialog = this._modalService.show(
        CreateTeacherComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditTeacherDialog = this._modalService.show(
        EditTeacherComponent,
        {
          class: 'modal-lg',
          initialState: {
            teacherid: id,
          },
        }
      );
    }

    createOrEditTeacherDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }
  viewCourse(stud: TeacherDto): void {

   this.showViewCourseDialog(stud.id);
  }

  showViewCourseDialog(id?: number): void {

    let showViewCourse: BsModalRef;
    if (!id) {
      showViewCourse = this._modalService.show(
        ViewcoursesComponent,
        {
          class: 'modal-lg',
        }
      );
    }
    else {

      showViewCourse = this._modalService.show(
        ViewcoursesComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }
  }
  registerCourse(teacher: TeacherDto): void {

    this.showRegisterCourseDialog(teacher.id);
  }
  showRegisterCourseDialog(teacherid?: any): void {

    let AddCourse: BsModalRef;
    if (teacherid) {
      AddCourse = this._modalService.show(
        AddcoursesComponent,
        {
          class: 'modal-lg',
           initialState: {
            teacherId: teacherid
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

