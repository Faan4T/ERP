import { Component, Injector } from '@angular/core';
import { PagedRequestDto, PagedListingComponentBase } from '@shared/paged-listing-component-base';
import { CourseDto, CourseServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs';
import { CreateCourseComponent } from './create-course/create-course.component';
import { EditCourseComponent } from './edit-course/edit-course.component';
class PagedCoursesRequestDto extends PagedRequestDto {
  keyword: string;
}
@Component({
  selector: 'courses',
  templateUrl: './courses.component.html',
  styles: [
  ]
})
export class CoursesComponent extends PagedListingComponentBase<CourseDto> {
  courses: CourseDto[] = [];
  keyword = '';
  searchText: CourseDto;
  page: Number = 1;
  count: Number = 5;


  constructor(
    injector: Injector,
    private _coursesService: CourseServiceProxy,
    private _modalService: BsModalService
  ) {
    super(injector);
  }

  list(
    request: PagedCoursesRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;

    this._coursesService
      .getAll(request.keyword,request.skipCount,request.maxResultCount)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result) => {
        this.courses = result;
      });
  }

  delete(course: CourseDto): void {
    abp.message.confirm(
      this.l('Deleting '+ course.courseName+" "+"("+course.courseCode+")"),
      undefined,
      (result: boolean) => {
        if (result) {
          this._coursesService
            .deleteCourse(course.id)
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

  createCourse(): void {
    this.showCreateOrEditCourseDialog();
  }

  editCourse(course: CourseDto): void {
    this.showCreateOrEditCourseDialog(course.id);
  }

  showCreateOrEditCourseDialog(id?: number): void {
    let createOrEditCourseDialog: BsModalRef;
    if (!id) {
      createOrEditCourseDialog = this._modalService.show(
        CreateCourseComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditCourseDialog = this._modalService.show(
        EditCourseComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }

    createOrEditCourseDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }
}


