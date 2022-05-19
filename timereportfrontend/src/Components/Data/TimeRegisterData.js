import Cookies from 'universal-cookie';

const cookies = new Cookies();

const jwt = cookies.get('usertoken');
const fetchOptions = {
    method: 'GET',
    headers: { 
        Accept: 'application/json',
        'Content-Type': 'application/json',
        'Authorization': "Bearer " +  jwt,
        'Access-Control-Allow-Origin': '*'
    }
}

export const fetchProjects = async (id) => {  
    const projectUrl = 'https://localhost:8080/Project/customer/' + id
    
    const response = await fetch(projectUrl, fetchOptions)  
    const json = await response.json()

    return json
}

export const fetchCustomers = async () => {
    const customerUrl = 'https://localhost:8080/customer'
    const response = await fetch(customerUrl, fetchOptions)  
    const json = await response.json()

    return json
}

export const postTimeRegister = async (data) => {
    const registerUrl = 'https://localhost:8080/timereport'
    const fetchOptionsPost = {
        method: 'POST',
        headers: { 
            Accept: 'application/json',
            'Content-Type': 'application/json',
            'Authorization': "Bearer " +  jwt,
            'Access-Control-Allow-Origin': '*'
        },
        body: JSON.stringify(data)
    }

    const response = await fetch(registerUrl, fetchOptionsPost) 
    const json = await response.status;
    
    return(json)
}

