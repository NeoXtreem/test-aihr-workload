import React, { useEffect, useState } from 'react';
import { DateTime } from 'luxon';

interface IStudent {
  id: number,
  name: string,
}

interface ICourse {
  id: number,
  name: string,
  hours: number,
}

interface ICalculation {
  startDate: DateTime,
  endDate: DateTime,
  numCourses: number,
  weeklyStudy: number,
}

const Workload: React.FC = () => {
  const [students, setStudents] = useState<IStudent[]>([]);
  const [courses, setCourses] = useState<ICourse[]>([]);
  const [selectedStudentId, setSelectedStudentId] = useState<number>();
  const [calculations, setCalculations] = useState<ICalculation[]>([]);
  const [loading, setLoading] = useState(true);
  const [startDate, setStartDate] = useState<DateTime>(DateTime.now());
  const [endDate, setEndDate] = useState<DateTime>(startDate.plus({ weeks: 1 }));
  const [courseSelection, setCourseSelection] = useState({});

  useEffect(() => {
    const initialize = async () => {
      setStudents(await (await fetch('workload/students')).json());
      setCourses(await (await fetch('workload/courses')).json());
      setLoading(false);
    }
    initialize();
  }, []);

  const updateSelectedStudent = (e: any) => {
    setSelectedStudentId(e.target.value);
  }

  const updateCourseSelection = (courseId: number) => {
    courseSelection[courseId] = !courseSelection[courseId];
    setCourseSelection(Object.assign({}, courseSelection));
  }

  const updateStartDate = (e: any): void => {
    setStartDate(DateTime.fromISO(e.target.value));
  }

  const updateEndDate = (e: any): void => {
    setEndDate(DateTime.fromISO(e.target.value));
  }

  const calculate = async (): Promise<void> => {
    const courseIds = courses.map((course: ICourse) => course.id).filter(id => courseSelection[id]);
    if (courseIds.length === 0) {
      alert('Please select at least one course.');
      return;
    }

    const weeklyStudy = await (await fetch('workload/calculate', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ studentId: selectedStudentId, startDate: startDate.toISODate(), endDate: endDate.toISODate(), courseIds })
    })).json();

    alert(`${weeklyStudy} hours of weekly study required for the selected courses between the specified dates. Scroll down for your calculation history.`);

    calculations.push({ startDate, endDate, numCourses: courseIds.length, weeklyStudy });
    setCalculations([...calculations]);
  }

  const renderCoursesTable = (courses: ICourse[]) => {
    return (
      <div>
        <table className='table table-striped' aria-labelledby="tabelLabel">
          <thead>
            <tr>
              <th>Course</th>
              <th>Hours</th>
            </tr>
          </thead>
          <tbody>
            {courses.map(course =>
              <tr key={course.id}>
                <td><input type="checkbox" id={`course-select-${course.id}`} checked={courseSelection[course.id]} onChange={() => updateCourseSelection(course.id)} /></td>
                <td><label htmlFor={`course-select-${course.id}`}>{course.name}</label></td>
                <td>{course.hours}</td>
              </tr>
            )}
          </tbody>
        </table>
        <table className='table table-striped' aria-labelledby="tabelLabel">
          <thead>
            <tr>
              <th>Start Date</th>
              <th>End Date</th>
              <th>Number of Courses</th>
              <th>Weekly Study (Hours)</th>
            </tr>
          </thead>
          <tbody>
            {calculations.map((calculation) =>
              <tr>
                <td>{calculation.startDate.toLocaleString()}</td>
                <td>{calculation.endDate.toLocaleString()}</td>
                <td>{calculation.numCourses}</td>
                <td>{calculation.weeklyStudy}</td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    );
  }

  const contents = loading ? <p><em>Loading...</em></p> : renderCoursesTable(courses);
  const style = { padding: 5 };

  return (
    <div>
      <h1 id="tableLabel">Instructions</h1>
      <p>Please select your name from the dropdown, then select the courses you wish to study, and the period of study (minimum 1 week). You may then calculate your weekly study hours based on the courses selected. A history of workload calculations is displayed below the Courses table, but this is per-session only. The latest calculation for a given student is always persisted in the database.</p>
      <div style={style}>
        <select value={selectedStudentId} onChange={updateSelectedStudent}>
          <option>Select Student</option>
          {students.map((student) => {
            return (<option key={student.id} value={student.id}>{student.name}</option>);
          })}
        </select>
      </div>
      <div>
        <label style={style} htmlFor="startDate">Start date:</label>
        <input type="date" id="startDate" value={startDate.toISODate()} max={endDate.minus({ weeks:1 }).toISODate()} onChange={updateStartDate} />
        <label style={style} htmlFor="endDate">End date:</label>
        <input type="date" id="endDate" value={endDate.toISODate()} min={startDate.plus({ weeks: 1 }).toISODate()} onChange={updateEndDate} />
        <button type="button" disabled={selectedStudentId === undefined} onClick={calculate}>Calculate</button>
      </div>
      <h1 id="tableLabel">Courses</h1>
      {contents}
    </div>
  );
}

Workload.displayName = Workload.name;
export default Workload;
