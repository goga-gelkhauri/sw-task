Select Teacher.Id,Teacher.Name,Teacher.LastName,Teacher.Gender,Teacher.Object from Teacher 
inner join Teacher_Pupil ON Teacher.Id = Teacher_Pupil.T_Id 
inner join Pupil on Teacher_Pupil.P_Id = Pupil.Id
where Pupil.Name = 'giorgi'