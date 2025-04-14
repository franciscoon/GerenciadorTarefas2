import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { api } from "../services/api";
import "../assets/css/TaskDetails.css"
import {formatStatus} from "../utils/utils.js"

const formatDate = (dateString) => {
  if (!dateString) return "Sem data"; 

  const date = new Date(dateString); 
  if (isNaN(date)) return "Data inválida"; 

  const day = String(date.getDate()).padStart(2, "0"); 
  const month = String(date.getMonth() + 1).padStart(2, "0"); 
  const year = date.getFullYear(); 

  return `${day}-${month}-${year}`; 
};



export function TaskDetails(){
    const {id} = useParams();
    const [tarefa, setTarefa] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        async function fetchTask() {
            try{
                const response = await api.get(`/tarefas/${id}`);
                setTarefa(response.data);
            }catch(error){
                console.error("Erro ao carregar tarefa:", error);
            }
        }

        fetchTask();
    }, [id]);

    if(!tarefa) return <div>Carregando...</div>;

    return (
        <div className="task-details-container">
          <h2>Detalhes da Tarefa</h2>
          <p><strong>Título:</strong> {tarefa.titulo}</p>
          <p><strong>Descrição:</strong> {tarefa.descricao}</p>
          <p><strong>Status:</strong> {formatStatus(tarefa.status)}</p>
          <p><strong>Data de Criação:</strong> {formatDate(tarefa.dataCriacao)}</p>
          <p><strong>Data de Conclusão:</strong> {formatDate(tarefa.dataConclusao)}</p>
    
          <button className="botao-voltar-home" onClick={() => navigate("/")}>
            Voltar
          </button>
        </div>
      );    
}