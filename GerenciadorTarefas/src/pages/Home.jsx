import { useEffect, useState } from "react";
import { api } from "../services/api";
import { TaskFormModal } from "../components/TaskFormModal";
import {TaskList} from "../components/TaskList";
import { useNavigate } from "react-router-dom";
import "../assets/css/Home.css"

export function Home(){
    const [tarefas, setTarefas] = useState([]);
    const [showModal, setShowmodal] = useState(false);
    const [tarefaSelecionada, setTarefaSelecionada] = useState(null);
    const [filtroStatus, setFiltroStatus] = useState('');
    const navigate = useNavigate();

    async function fetchTarefas(){
        try{
            const response = await api.get("tarefas");
            setTarefas(response.data);
        } catch(error){
            console.error("Erro ao buscar tarefas", error);
        }
    }

    useEffect(() => {
        fetchTarefas();
    }, []);

    function handleAdicionar(){
        setTarefaSelecionada(null);
        setShowmodal(true);
    }

    function handlerEditar(tarefa){
        setTarefaSelecionada(tarefa);
        setShowmodal(true);
    }

    async function handleExcluir(id) {
        try{
            await api.delete(`/tarefas/${id}`);
            fetchTarefas();
        }
        catch(error){
            console.error("Erro ao Excluir tarefa:", error);
        }
    }

    function handleVer(id){
        navigate(`/detalhes/${id}`);
    }

    const tarefasFiltradas = filtroStatus
                            ? tarefas.filter(t => t.status === filtroStatus)
                            : tarefas;

    return (
        <div className="home-container">
            <h1 className="title">Tarefas</h1>
            <button className="btn-adicionar" onClick={handleAdicionar}>Adicionar Tarefa</button>

            <div className="filtro-status">
                <label htmlFor="status">Filtrar por status: </label>
                <select
                    id="status"
                    value={filtroStatus}
                    onChange={(e) => setFiltroStatus(e.target.value)}
                >
                    <option value="">Todos</option>
                    <option value="Pendente">Pendente</option>
                    <option value="EmProgresso">Em Progresso</option>
                    <option value="Concluida">Concluída</option>
                </select>
            </div>

            <TaskList
                tarefas={tarefasFiltradas}
                onEditar={handlerEditar}
                onExcluir={handleExcluir}
                onVer={handleVer}
            />

            {showModal && (
                <TaskFormModal
                    tarefa={tarefaSelecionada}
                    onClose={() => setShowmodal(false)}
                    onTarefaCriadaOuEditada={() => {
                        setShowmodal(false);
                        fetchTarefas();
                    }}
                />
            )}
        </div>
    );
}
