<template>
    <div>
        <div class=" d-flex justify-content-end p-1 sticky-top">
            <b-button size="sm" variant="warning" @click="goBack()"
                >Go back</b-button
            >
        </div>
        <div class=" d-flex w-100 ">
            <loading :isLoading="isLoading" :color="'primary'" />
            <div class="w-100 justify-content-center">
                <div class="d-flex w-100 title">
                    <div class="flex-fill">ImageBoundary</div>
                    <div class="flex-fill">AdaptiveCard</div>
                </div>
                <RenderImageItem
                    v-for="(item, index) in templates"
                    :key="index"
                    :url="item"
                    :id="index.toString()"
                >
                </RenderImageItem>
                <!-- <div v-if="isError" class="justify-content-center mt-2 p-2">
                <b-alert
                    show
                    variant="warning"
                    dismissible
                    @dismissed=";(isError = false), getTemplateImages()"
                >
                    {{ error }} please try again.!
                </b-alert>
            </div>

            <div
                v-else
                v-for="(item, index) in base64_images"
                :key="index"
                class="col-lg-3 col-md-4 col-6 "
                @click="openDetailView(item)"
            >
                {{ index }}
            </div> -->
            </div>
        </div>
    </div>
</template>
<script>
import Loader from '../../components/loader/Loader.vue'
import RenderImageItem from '../render-all-image/RenderImageItem'
import imageapi from '@/services/ImageApi.js'
import { mapState } from 'vuex'
export default {
    name: 'RenderImage',
    components: {
        loading: Loader,
        RenderImageItem: RenderImageItem
    },
    data() {
        return {
            isLoading: false,
            base64_images: null,
            isError: false,
            error: ''
        }
    },
    computed: mapState({
        templates: state => state.pic2card.base64_images
    }),
    methods: {
        goBack: function(value) {
            this.$router.push({
                name: 'Pic2Card'
            })
        },
        openDetailView: function(value) {
            this.$router.push({
                name: 'cardDetailView',
                params: {
                    url: value
                }
            })
        },
        getTemplateImages: function() {
            console.log('calling get template image api')
            this.isLoading = true
            imageapi
                .getTemplateImages()
                .then(response => {
                    let cards = response.data['templates']
                    this.$store.dispatch('saveTemplateImages', cards)
                    this.isLoading = false
                })
                .catch(err => {
                    console.log(err)
                    this.isLoading = false
                    this.error = 'Something Went Wrong'
                    this.isError = true
                })
        }
    },

    filters: {
        image_data_url(value) {
            return 'data:image/png;base64,' + value
        }
    },
    created() {
        if (this.templates.length == 0) {
            this.getTemplateImages()
        }
    }
}
</script>
<style lang="scss" scoped>
.p2c-thumbnail {
    cursor: pointer;
}
.title {
    font-size: 1.2rem;
    font-variant-caps: petite-caps;
    font-weight: bold;
    color: #505050;
    padding: 0.5rem;
}
</style>
