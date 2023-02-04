import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { StudentWithCourse, CourseDto, StudentServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'view-courses',
  templateUrl: './view-courses.component.html',
  styles: [
  ]
})
export class ViewCoursesComponent extends AppComponentBase implements OnInit {
saving = false;
studentcourses: StudentWithCourse[]=[];
course : CourseDto[];
id: number;
courseid: number;
rollno:string;
result: any;
i:[];
constructor(
  injector: Injector,
  public _studentService: StudentServiceProxy,
  public bsModalRef: BsModalRef
) {
  super(injector);
}

ngOnInit(): void {
  debugger
  this._studentService.getCourses(this.id).subscribe((result) => {debugger
    this.studentcourses= result;
    this.result = result;
     });
}

DropCourse(student: StudentWithCourse): void {
 debugger
  this.showDropCourseDialog(student.courseId,student.studentId,student.courses.courseName) ;
}
showDropCourseDialog(courseId?: any,rollno?: any,courseName?:any): void {
  if (courseId != null) {
    debugger
        abp.message.confirm(
      this.l('Are You sure To drop This Course'+"( "+ courseName +" )"),
      undefined,
      (result: boolean) => {
        if (result) {
          debugger
          this._studentService.dropCourse(courseId,rollno).subscribe(() => {
            abp.notify.success(this.l('Dropped Successfully '));
            this._studentService.getCourses(this.id).subscribe((result) => {
              this.studentcourses= result;
          });
          });
        }

      }
    );

  }


}

DropAllCourse(): void {
debugger
  this.showDropAllCourseDialog(this.id, this.result);
}
showDropAllCourseDialog(id?: any,courseId?:any): void {
debugger
if(courseId!=0)
{
    abp.message.confirm(
      this.l('Are You sure To drop All Course'),
      undefined,
      (result: boolean) => {
        debugger;
        if (result==true) {
          debugger
          this._studentService.dropAllCourses(id).subscribe(() => {
            abp.notify.success(this.l('All Courses Dropped Successfully '));
            this._studentService.getCourses(this.id).subscribe((result) => {
              this.studentcourses= result;
            });
          });

        }
      }
    );
}
else{
  abp.notify.warn(this.l('No Courses Enrolled'));
}
}
}

