import React, { useState, useEffect } from 'react'
import { fetchCustomers, fetchProjects, postTimeRegister } from './Data/TimeRegisterData';

export const TimeRegisterForm = () => {
  const [projects, setProjects] = useState([])
  const [customers, setCustomers] = useState([])
  const [selectedProject, setSelectedProject] = useState(null)
  const [selectedCustomer, setSelectedCustomer] = useState(null)
  const [minutes, setMinutes] = useState(0)
  const [description, setDescription] = useState("")

  useEffect(()=>{
    fetchCustomers().then(result => {
      setCustomers(result)
      setSelectedCustomer(result[0].id)

      onSetSelectedCustomer(result[0].id)
    })
  },
  []);  

  const onSetSelectedCustomer = (id) => {
    fetchProjects(id).then(result => {
      setProjects(result)
      setSelectedProject(result[0].id)
    })
  }

  const onSave = () => {
    var data = {
      "minutes": minutes,
      "description": description,
      "projectid": selectedProject
    }

    postTimeRegister(data).then(result => {
      if(result.status == 201) {
        setDescription("")
        setMinutes(0)
      } else if(result.status == 400) {
        result.text().then(data => {
          window.alert(data)
        })
      }
    })
    

  }

  return (
    <div class="register">
      <div class="formDesign">
        <form class="form">
            <label >Customer:</label>
            <select class="inputField" onChange={e=>onSetSelectedCustomer(e.target.value)}>
              {customers.map(customer => 
                <option key={customer.id} value={customer.id}>{customer.name}</option>
                )}
            </select>
            <label>Project:</label>
            <select class="inputField">
              {projects.map(project => 
                <option key={project.id} value={project.id}>{project.name}</option>
                )}
            </select>

            <label>Minutes:</label>
            <input class="inputField" type="number" value={minutes} onChange={e => setMinutes(e.target.value)}/>

            <label>Description:</label>
            <textarea class="inputField" rows="4" cols="30" value={description} onChange={e=>setDescription(e.target.value)}></textarea>

            <button type="button" class="saveButton" onClick={onSave}>Save report</button>
        </form>
        </div>
    </div>
  )
}
