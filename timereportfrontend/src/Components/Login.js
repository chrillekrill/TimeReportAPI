import React, { useState, useEffect} from 'react'
import {fetchLogin} from './Data/LoginData'

export const Login = () => {
    const [user, setUser] = useState({});
    
    useEffect(() => {
        fetchLogin("christoffer","Hejsan123#").then(result => {
            setUser(result)
            console.log(user)
        }).catch(error => {
            console.log(error)
        })
    },[])

  return (
    <div>
        <h1>Logga in</h1>
    </div>
  )
}
