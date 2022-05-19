import React, { useState, useEffect} from 'react'

export const Header = ({loggedInStatus, checkStatus}) => {

  const logout = () => {
    localStorage.removeItem("user")
    loggedInStatus(false)
  }

  return (
    <div class="header">
    <a href="#default" class="logo">Time register</a>
    {checkStatus ? <a class="logo" onClick={logout}>Logout</a> : ""}
    </div>
  )
}
