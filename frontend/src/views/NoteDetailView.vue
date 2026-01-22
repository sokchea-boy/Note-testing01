<template>
  <div v-if="note" class="container mx-auto px-4 py-8">
    <NoteDetail :note="note" />
  </div>
  <div v-else-if="loading" class="text-center py-8">Loading...</div>
  <div v-else class="text-center py-8 text-red-600">Note not found</div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import NoteDetail from '@/components/NoteDetail.vue'
import { useNotesStore } from '@/stores/notesStore'
import { getNoteByIdApi } from '@/composables/useNotes'
import type { Note } from '@/types'

const route = useRoute()
const notesStore = useNotesStore()

const loading = ref(false)
const note = ref<Note | null>(null)

const noteId = computed(() => route.params.id as string)

onMounted(async () => {
  // First check if note is in store
  const storedNote = notesStore.getNoteById(noteId.value)
  if (storedNote) {
    note.value = storedNote
  } else {
    // Fetch from API
    loading.value = true
    try {
      note.value = await getNoteByIdApi(noteId.value)
    } catch (error) {
      console.error('Failed to fetch note:', error)
    } finally {
      loading.value = false
    }
  }
})
</script>