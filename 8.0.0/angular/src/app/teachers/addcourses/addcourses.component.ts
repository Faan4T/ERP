import { Component, OnInit } from '@angular/core';
import { CourseDto, GetAllCoursesToAssign, TeacherServiceProxy, TeacherDto } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'addcourses',
  templateUrl: './addcourses.component.html',
  styles: [
  ]
})
export class AddcoursesComponent implements OnInit {
  title?: string;
  closeBtnName?: string;
  onSave?: string;
  list: any[] = [];
  checkedIDs :any= [];
  teacher: TeacherDto[];
  course: CourseDto[] = [];
  getcourseforRegister: GetAllCoursesToAssign[]=[]
  selectedcourseforRegister: GetAllCoursesToAssign[]=[]
  teacherId: any;
  id: number;

  constructor(
    public bsModalRef: BsModalRef,
    private _teacherService: TeacherServiceProxy,
    ) {}

  ngOnInit() {

    this._teacherService
      .getRegisteredCourses(this.id)
      .subscribe((result)=>{
        this.getcourseforRegister = result
      })
  }

  changeSelection() {

    this.fetchSelectedCourses()
    this.fetchCheckedCourseIDs()
  }

  fetchSelectedCourses() {

    this.selectedcourseforRegister = this.getcourseforRegister.filter((value, index) => {
      return value.check
    });
  }

  fetchCheckedCourseIDs() {

    this.checkedIDs = []
    this.getcourseforRegister.forEach((value, index) => {
      if (value.check) {
        this.checkedIDs.push(value.id);
      }
    });
  }
  AddCourse() {

    this._teacherService
      .registerCourses(this.teacherId,this.checkedIDs)
      .subscribe(() => {
        abp.notify.success('Courses Added Successfully ');
      })
  }
}
