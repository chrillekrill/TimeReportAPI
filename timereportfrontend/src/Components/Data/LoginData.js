const url = 'https://localhost:8080/user/login'

export const fetchLogin = async (name, password) => {
    const fetchOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', 'Access-Control-Allow-Origin': '*' },
        body: JSON.stringify({ Username: name, Password: password})
    }
    const response = await fetch(url, fetchOptions)
    const json = await response.json()
    
    return json
}
