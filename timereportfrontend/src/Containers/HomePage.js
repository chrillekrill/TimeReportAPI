import React, { useState, useEffect} from 'react'
import Cookies from 'universal-cookie';
import {Header} from '../Components/Header'
import {Footer} from '../Components/Footer'
import { Login } from '../Components/Login'
import { TimeRegisterForm } from '../Components/TimeRegisterForm'

export const HomePage = () => {
    const [loggedIn, setLoggedIn] = useState(false);

    let checkLoggedIn = (status) => {
      setLoggedIn(status);
    }

    useEffect(() => {
      const loggedInUser = localStorage.getItem("user");

      if(loggedInUser) {
        const foundUser = JSON.parse(loggedInUser);   
        const cookies = new Cookies();
        cookies.set('usertoken', foundUser.jwt);
        setLoggedIn(true);
      } else {
        setLoggedIn(false);
      }
        
    },[])
  return (
      <div>
        <Header />
        {loggedIn ? <TimeRegisterForm /> : <Login loggedInStatus={checkLoggedIn}/>}        
        <Footer />
      </div>
  )
}
