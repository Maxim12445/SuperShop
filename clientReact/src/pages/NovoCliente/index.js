import React, {useState, useEffect} from 'react';
import './styles.css';
import {Link, useNavigate, useParams} from 'react-router-dom';
import { FiCornerDownLeft , FiUserPlus } from 'react-icons/fi';
import api from '../../services/api';

export default function NovoCliente(){
    
    const [id, setId] = useState(null);
    const [nome, setNome] = useState('');
    const [apelido, setApelido] = useState('');
    const [nif, setNif] = useState('');
    const [morada, setMorada] = useState('');
    const [telefone, setTelefone] = useState('');

    const {clienteId} = useParams();
    const navigate = useNavigate();


    const token = localStorage.getItem('token');
    const authorization = {
        headers: {
            Authorization : `Bearer ${token}`,
        }
    }

    useEffect(()=>{
        if(clienteId === '0')
            return;
        else
            loadCliente();
}, clienteId)

    async function loadCliente(){
        try{

            const response = await api.get(`api/clientes/${clienteId}`, authorization);

            setId(response.data.id);
            setNome(response.data.nome);
            setApelido(response.data.apelido);
            setNif(response.data.nif);
            setMorada(response.data.morada);
            setTelefone(response.data.telefone);

        }catch(error){
            alert('Erro ao recuperar o cliente ' + error);
            navigate('/clietes');
        }
    }

    async function saveOrUpdate(event) {
        event.preventDefault();


        const data = {
            nome,
            apelido,
            nif,
            morada,
            telefone
        }

        try{
            if(clienteId==='0'){
                await api.post('api/clientes',data, authorization);
            }else{
                data.id=id;
                await api.put(`api/clientes/${id}`,data, authorization);
            }
        }catch(error){
            alert('Erro ao guavar cliente' + error);
        }
        navigate('/clientes');

    }
    
    return(
        <div className='novo-cliente-container'>
            <div className="Content">
                
                <section className='form'>
                    <FiUserPlus size="105" color="#17202a"/>
                    <h1>{clienteId === '0'? 'Incluir Novo Cliente' : 'Atualizar Cliente'}</h1>
                    <Link className="back-link" to="/clientes">
                        <FiCornerDownLeft size="25" color="#17202a"/>
                        Retornar
                    </Link>
                </section>
                <form onSubmit={saveOrUpdate}>
                    <input placeholder='Nome' 
                        value={nome}
                        onChange={e=>setNome(e.target.value)}/>
                    <input placeholder="Apelido" 
                        value={apelido}
                        onChange={e=>setApelido(e.target.value)}/>
                    <input placeholder="Nif" 
                        value={nif}
                        onChange={e=>setNif(e.target.value)}/>
                    <input placeholder="Morada" 
                        value={morada}
                        onChange={e=>setMorada(e.target.value)}/>
                    <input placeholder="Telefone" 
                        value={telefone}
                        onChange={e=>setTelefone(e.target.value)}/>
                    <button className='button' type="submit">{clienteId === '0'? 'Incluir' : 'Atualizar'}</button>
                </form>
            </div>
        </div>
    )
}