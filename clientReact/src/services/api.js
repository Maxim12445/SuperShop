import axios from 'axios';

const api = axios.create({
    baseURL : "https://localhost:7042",
})

export default api;