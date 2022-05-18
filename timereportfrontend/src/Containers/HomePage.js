import React, { useState, useEffect} from 'react'
import {Header} from '../Components/Header'
import {Footer} from '../Components/Footer'
import { Login } from '../Components/Login'

export const HomePage = () => {
    const [loggedIn, setLoggedIn] = useState(Boolean);

    useEffect(() => {
        setLoggedIn(false);
    },[])
  return (
      <div>
        <Header />
        {loggedIn ? <div></div> : <Login />}        
        <Footer />
      </div>
  )
}
