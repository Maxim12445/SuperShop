import React, {useState, useEffect} from 'react';
import { Link, useHistory, useNavigate } from 'react-router-dom';
import './styles.css';
import api from '../../services/api';

import logoCadastro from '../../assets/cadastro1.png';
import { FiXCircle, FiEdit, FiUserX } from 'react-icons/fi';


export default function Clientes(){
    
    //filtrar dados
    const [searchInput, setSearchInput] = useState('');
    const [filtro, setFiltro] = useState([]);

    const [clientes, setClientes] = useState([]);

    const email = localStorage.getItem('email');
    const token = localStorage.getItem('token');

    const navigate = useNavigate();

    const authorization = {
        headers: {
            Authorization : `Bearer ${token}`,
        }
    }

    const searchClientes = (searchValue) => {
        setSearchInput(searchValue);
        if (searchInput !== '') {
            const dadosFiltrados = clientes.filter((item) => {
                return Object.values(item).join('').toLowerCase()
                .includes(searchInput.toLowerCase())
            });
        setFiltro(dadosFiltrados);
        }else{
            setFiltro(clientes);
        }
    }

    useEffect( ()=> {
        if (!token) {
            navigate('/');
        }
        try{
        api.get('api/clientes', authorization).then(
            response=> {setClientes(response.data);
            }, token)
        }catch(error){
            console.error("Erro ao buscar clientes:", error);
            if (error.response && error.response.status === 401) {
                // Token inválido ou expirado → volta para login
                localStorage.clear();
                navigate('/');
            } else {
                alert("Erro ao buscar clientes");
            }
        }
    })

    async function logout(){
        try{
            localStorage.clear();
            localStorage.setItem('token','');
            authorization.headers='';
            navigate('/');
        }catch(err){
            alert('Não foi possível fazer logout' + err);
        }
    }

    async function editCliente(id){
        try{
            navigate(`/cliente/novo/${id}`);
        }catch(error){
            alert('Não foi possível editar o aluno')
        }
    }

    async function deleteCliente(id){
        try{
            if(window.confirm('Deseja deletar o clientes de id = ' + id + ' ?'))
            {
                await api.delete(`api/clientes/${id}`, authorization);
                setClientes(clientes.filter(cliente => cliente.id !== id));
            }
        }catch(error){
            alert('Não foi possível excluir o cliente')
        }
    }

    
    return(
        <div className='cliente-container'>
            <header>
                <img src={logoCadastro} alt="Cadastro" />
                <span>Bem-Vindo!, <strong>{email}</strong></span>
                <Link className='button' to="/cliente/novo/0">Novo Cliente</Link>
                
                <button onClick={logout} type='button'>
                    <FiXCircle size={35} color="#17202a"/>
                </button>
            </header>
            <form>
                <input type='text' 
                placeholder='filtrar por nome...'
                onChange={(e) => searchClientes(e.target.value)}/>
            </form>

            <h1>Relação de Clientes</h1>
            {searchInput.length > 1 ? (
                <ul>
                {filtro.map(cliente=>(
                    <li key={cliente.id}>
                        <b>Nome:</b>{cliente.nome}<br/><br/>
                        <b>Apelido:</b>{cliente.apelido}<br/><br/>
                        <b>Nif:</b>{cliente.nif}<br/><br/>
                        <b>Morada:</b>{cliente.morada}<br/><br/>
                        <b>Telefone:</b>{cliente.telefone}<br/><br/>
                    <button onClick={()=> editCliente(cliente.id)} type='button'>
                        <FiEdit size="25" colors="#17202a" />
                    </button>
                    <button type='button' onClick={()=> deleteCliente(cliente.id)}>
                        <FiUserX size="25" colors="#17202a" />
                    </button>
                </li>
                ))}
            </ul>
            ) : (
            <ul>
                {clientes.map(cliente=>(
                    <li key={cliente.id}>
                        <b>Nome:</b>{cliente.nome}<br/><br/>
                        <b>Apelido:</b>{cliente.apelido}<br/><br/>
                        <b>Nif:</b>{cliente.nif}<br/><br/>
                        <b>Morada:</b>{cliente.morada}<br/><br/>
                        <b>Telefone:</b>{cliente.telefone}<br/><br/>
                    <button onClick={()=> editCliente(cliente.id)} type='button'>
                        <FiEdit size="25" colors="#17202a" />
                    </button>
                    <button type='button' onClick={()=> deleteCliente(cliente.id)}>
                        <FiUserX size="25" colors="#17202a" />
                    </button>
                </li>
                ))}
            </ul>
            )}
        </div>
    )
}