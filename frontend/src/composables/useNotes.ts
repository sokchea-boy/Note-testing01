import type { Note, CreateNoteDto, UpdateNoteDto } from '@/types'

const API_BASE_URL = 'https://localhost:5001/api/notes'

export async function fetchNotesApi(): Promise<Note[]> {
  const headers: Record<string, string> = {}
  const token = localStorage.getItem('token')
  if (token) headers['Authorization'] = `Bearer ${token}`

  const response = await fetch(API_BASE_URL, {
    headers
  })
  if (!response.ok) {
    throw new Error('Failed to fetch notes')
  }
  return response.json()
}

export async function createNoteApi(dto: CreateNoteDto): Promise<Note> {
  const headers: Record<string, string> = { 'Content-Type': 'application/json' }
  const token = localStorage.getItem('token')
  if (token) headers['Authorization'] = `Bearer ${token}`

  const response = await fetch(API_BASE_URL, {
    method: 'POST',
    headers,
    body: JSON.stringify(dto)
  })
  if (!response.ok) {
    throw new Error('Failed to create note')
  }
  return response.json()
}

export async function updateNoteApi(id: string, dto: UpdateNoteDto): Promise<Note> {
  const headers: Record<string, string> = { 'Content-Type': 'application/json' }
  const token = localStorage.getItem('token')
  if (token) headers['Authorization'] = `Bearer ${token}`

  const response = await fetch(`${API_BASE_URL}/${id}`, {
    method: 'PUT',
    headers,
    body: JSON.stringify(dto)
  })
  if (!response.ok) {
    throw new Error('Failed to update note')
  }
  return response.json()
}

export async function deleteNoteApi(id: string): Promise<void> {
  const headers: Record<string, string> = {}
  const token = localStorage.getItem('token')
  if (token) headers['Authorization'] = `Bearer ${token}`

  const response = await fetch(`${API_BASE_URL}/${id}`, {
    method: 'DELETE',
    headers
  })
  if (!response.ok) {
    throw new Error('Failed to delete note')
  }
}

export async function getNoteByIdApi(id: string): Promise<Note> {
  const headers: Record<string, string> = {}
  const token = localStorage.getItem('token')
  if (token) headers['Authorization'] = `Bearer ${token}`

  const response = await fetch(`${API_BASE_URL}/${id}`, {
    headers
  })
  if (!response.ok) {
    throw new Error('Failed to fetch note')
  }
  return response.json()
}