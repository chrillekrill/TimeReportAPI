import React, { useState, useEffect} from 'react'
import Cookies from 'universal-cookie';
import {fetchLogin} from './Data/LoginData'

export const Login = ({loggedInStatus}) => {
    const [user, setUser] = useState({});
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");

    const LoginButton = () => {

        fetchLogin(username, password).then(result => {
                setUser(result)
                loggedInStatus(true);
                
                let localstorageItem = {
                    "username": result.username,
                    "jwt": result.jwt
                }
                localStorage.setItem("user", JSON.stringify(localstorageItem))
                
                const cookies = new Cookies();
                cookies.set('usertoken', result.jwt);
            }).catch(error => {
                console.log(error)
            })
    }

  return (
    <div>
        <form>
            <label>Username:</label>
            <input onChange={e => setUsername(e.target.value)} />
            <label>password:</label>
            <input type="password" onChange={e => setPassword(e.target.value)} />

            <button type="button" onClick={LoginButton}>login</button> 
        </form>
    </div>
  )
}
