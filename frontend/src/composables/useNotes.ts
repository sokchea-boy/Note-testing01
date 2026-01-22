import type { Note, CreateNoteDto, UpdateNoteDto } from '@/types'

const API_BASE_URL = 'https://localhost:5001/api/notes'

export async function fetchNotesApi(): Promise<Note[]> {
  const response = await fetch(API_BASE_URL)
  if (!response.ok) {
    throw new Error('Failed to fetch notes')
  }
  return response.json()
}

export async function createNoteApi(dto: CreateNoteDto): Promise<Note> {
  const response = await fetch(API_BASE_URL, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(dto)
  })
  if (!response.ok) {
    throw new Error('Failed to create note')
  }
  return response.json()
}

export async function updateNoteApi(id: string, dto: UpdateNoteDto): Promise<Note> {
  const response = await fetch(`${API_BASE_URL}/${id}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(dto)
  })
  if (!response.ok) {
    throw new Error('Failed to update note')
  }
  return response.json()
}

export async function deleteNoteApi(id: string): Promise<void> {
  const response = await fetch(`${API_BASE_URL}/${id}`, {
    method: 'DELETE'
  })
  if (!response.ok) {
    throw new Error('Failed to delete note')
  }
}

export async function getNoteByIdApi(id: string): Promise<Note> {
  const response = await fetch(`${API_BASE_URL}/${id}`)
  if (!response.ok) {
    throw new Error('Failed to fetch note')
  }
  return response.json()
}